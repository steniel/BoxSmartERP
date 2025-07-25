using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP.Services
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)] 
    public class InfoTech
    {
        private readonly string _MSSQLConnection;
        public InfoTech(string connectionString)
        {
            _MSSQLConnection = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        /// <summary>
        /// Only machine from M21 to M28 can be used to create a Machine count for rubberdie and Diecutting
        /// Cu_Na, Pdl_Order, Ordsb_Boxname, Pdl_Boxno, Mch_Name, Pdl_Mchno, Pdl_Trgqty, Ordsb_Qty, Ordsb_Ordsize, Pdl_Date, pdl_Adduser
        /// </summary>
        public class MacScheduleItem
        {
            public string cu_Na { get; set; }
            public string pdl_Order { get; set; }
            public string ordsb_Boxname { get; set; }
            public string pdl_Boxno { get; set; }
            public string mch_Name { get; set; }
            public string pdl_Mchno { get; set; }
            public int pdl_Trgqty { get; set; }
            public int ordsb_Qty { get; set; }
            public string ordsb_Ordsize { get; set; }
            public string pdl_Date { get; set; }
            public string pdl_Adduser { get; set; }
        }

        public class DataTableResponse
        {
            public int Draw { get; set; }
            public int RecordsTotal { get; set; }
            public int RecordsFiltered { get; set; }
            public List<MacScheduleItem> Data { get; set; }
        }

        public string GetMachineScheduleDataTable(
            int draw,       // DataTables draw counter
            int start,      // DataTables offset (skip)
            int length,     // DataTables limit (take)           
            string searchValue) // Global search value from DataTables
        {
            List<MacScheduleItem> ScheduledItems = new List<MacScheduleItem>();
            int recordsTotal = 0;    // Total records in DB without any filters
            int recordsFiltered = 0; // Total records after applying date/search filters                                     

            string baseWhereClause = " Where Pdl_Id = N'1' And Pdl_Instock = N'True' And Pdl_Stked = N'False' And ( Ordsb_WipType = N'1' AND TRIM(pdl_Mchno) BETWEEN 'M21' AND 'M28' )";            
            string searchFilterClause = "";
           

            // Build search filter clause if search value is provided
            bool hasSearch = !string.IsNullOrEmpty(searchValue);
            if (hasSearch)
            {           //cu_Na, pdl_Order, ordsb_Boxname, pdl_Boxno, mch_Name, pdl_Mchno, pdl_Trgqty, ordsb_Qty, ordsb_Ordsize, pdl_Date, pdl_Adduser    
                searchFilterClause = $@"
                    AND (
                        LOWER(cu_Na) LIKE LOWER(@searchValue) OR
                        LOWER(pdl_Order) LIKE LOWER(@searchValue) OR
                        LOWER(ordsb_Boxname) LIKE LOWER(@searchValue) OR
                        LOWER(pdl_Boxno) LIKE LOWER(@searchValue) OR
                        LOWER(mch_Name) LIKE LOWER(@searchValue) OR
                        LOWER(pdl_Mchno) LIKE LOWER(@searchValue) OR
                        CAST(pdl_Trgqty AS NVARCHAR) LIKE @searchValue OR
                        CAST(ordsb_Qty AS NVARCHAR) LIKE @searchValue OR
                        LOWER(ordsb_Ordsize) LIKE LOWER(@searchValue) OR
                        LOWER(pdl_Date) LIKE LOWER(@searchValue) OR
                        LOWER(pdl_Adduser) LIKE LOWER(@searchValue)
                    )";
            }

            try
            {
                using (var conn = new SqlConnection(_MSSQLConnection))
                {
                    conn.Open();

                    // 1. Get total records (without any filters)
                    string countTotalQuery = @"
                                with fdata as (
                                    Select f2.Fld_Id, f1.Fld_Itemdata as Fld_DataId, f2.Fld_Itemdata
                                    From Field_Data_EN f1
                                    Inner Join Field_Data_EN f2 On f1.Fld_Id=f2.Fld_Id And f2.Fld_Itemnumber=2 And f2.Fld_Dataid=f1.Fld_Dataid
                                    Where f1.Fld_Itemnumber=1
                                )
                                Select count(*) From Prodlst
                                Inner Join Orders On Ord_No=Pdl_Order And Ord_Id=Pdl_Id
                                Inner Join Ordsubbox On Ordsb_No=Pdl_Order And Ordsb_Seq=Pdl_Ordseq And Ordsb_Id=Pdl_Id
                                Inner Join Customer On Cu_No=Ord_Cust And Cu_Id=Pdl_Id
                                Inner Join Machine On Mch_No=Pdl_MchNo And Mch_Id=Pdl_Id
                                Left Outer Join fdata f1 On f1.Fld_Id=N'Box_GoodsType' And f1.Fld_Dataid=Ordsb_GoodsType
                                Left Outer Join fdata f2 On f2.Fld_Id=N'Box_WipType' And f2.Fld_Dataid=Ordsb_WipType
                                Left Outer Join (
                                    Select Brc_Order, Brc_OrdSeq, Brc_SchId, Brc_Id, Sum(Brc_Qty) as Brc_Qty
                                    From BarcodePkg
                                    Where Brc_Status in ('0','6') And Brc_StkTime Is Null And Brc_PalletNo<>''
                                    Group By Brc_Order, Brc_OrdSeq, Brc_SchId, Brc_Id
                                    ) as m1 On Brc_Order=PdL_Order And Brc_OrdSeq=PdL_OrdSeq And Brc_SchId=PdL_SchId And Brc_Id=PdL_Id
                                Where Pdl_Id = N'1' And Pdl_Instock = N'True' And Pdl_Stked = N'False' And ( Ordsb_WipType = N'1' AND TRIM(pdl_Mchno) BETWEEN 'M21' AND 'M28')";
                    using (var cmd = new SqlCommand(countTotalQuery, conn))
                    {
                        recordsTotal = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    // 2. Get filtered records count (search filter if present)
                    string countFilteredQuery = $@"
                                    with fdata as (
                                        Select f2.Fld_Id, f1.Fld_Itemdata as Fld_DataId, f2.Fld_Itemdata
                                        From Field_Data_EN f1
                                        Inner Join Field_Data_EN f2 On f1.Fld_Id=f2.Fld_Id And f2.Fld_Itemnumber=2 And f2.Fld_Dataid=f1.Fld_Dataid
                                        Where f1.Fld_Itemnumber=1
                                    )
                                    Select count(*) From Prodlst
                                    Inner Join Orders On Ord_No=Pdl_Order And Ord_Id=Pdl_Id
                                    Inner Join Ordsubbox On Ordsb_No=Pdl_Order And Ordsb_Seq=Pdl_Ordseq And Ordsb_Id=Pdl_Id
                                    Inner Join Customer On Cu_No=Ord_Cust And Cu_Id=Pdl_Id
                                    Inner Join Machine On Mch_No=Pdl_MchNo And Mch_Id=Pdl_Id
                                    Left Outer Join fdata f1 On f1.Fld_Id=N'Box_GoodsType' And f1.Fld_Dataid=Ordsb_GoodsType
                                    Left Outer Join fdata f2 On f2.Fld_Id=N'Box_WipType' And f2.Fld_Dataid=Ordsb_WipType
                                    Left Outer Join (
                                        Select Brc_Order, Brc_OrdSeq, Brc_SchId, Brc_Id, Sum(Brc_Qty) as Brc_Qty
                                        From BarcodePkg
                                        Where Brc_Status in ('0','6') And Brc_StkTime Is Null And Brc_PalletNo<>''
                                        Group By Brc_Order, Brc_OrdSeq, Brc_SchId, Brc_Id
                                        ) as m1 On Brc_Order=PdL_Order And Brc_OrdSeq=PdL_OrdSeq And Brc_SchId=PdL_SchId And Brc_Id=PdL_Id                                    
                                        {baseWhereClause} {searchFilterClause};";
                    using (var cmd = new SqlCommand(countFilteredQuery, conn))
                    {                        
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        recordsFiltered = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Get the actual paged and filtered data
                    // In the GetMachineScheduleDataTable method, modify the dataQuery:
                    string dataQuery = $@"
                                    with fdata as (
                                        Select f2.Fld_Id, f1.Fld_Itemdata as Fld_DataId, f2.Fld_Itemdata
                                        From Field_Data_EN f1
                                        Inner Join Field_Data_EN f2 On f1.Fld_Id=f2.Fld_Id And f2.Fld_Itemnumber=2 And f2.Fld_Dataid=f1.Fld_Dataid
                                        Where f1.Fld_Itemnumber=1
                                    )
                                    SELECT cu_Na, pdl_Order, ordsb_Boxname, pdl_Boxno, mch_Name, pdl_Mchno, 
                                           pdl_Trgqty, ordsb_Qty, ordsb_Ordsize, pdl_Date, pdl_Adduser
                                    FROM (
                                        SELECT ROW_NUMBER() OVER (ORDER BY Pdl_Date DESC) AS RowNum,
                                               Cu_Na as cu_Na, 
                                               Pdl_Order as pdl_Order, 
                                               Ordsb_Boxname as ordsb_Boxname,
                                               Pdl_Boxno as pdl_Boxno,
                                               Mch_Name as mch_Name,
                                               Pdl_Mchno as pdl_Mchno,
                                               Pdl_Trgqty as pdl_Trgqty,
                                               Ordsb_Qty as ordsb_Qty,
                                               Ordsb_Ordsize as ordsb_Ordsize,
                                               Pdl_Date as pdl_Date,
                                               pdl_Adduser as pdl_Adduser
                                        FROM Prodlst
                                        Inner Join Orders On Ord_No=Pdl_Order And Ord_Id=Pdl_Id
                                        Inner Join Ordsubbox On Ordsb_No=Pdl_Order And Ordsb_Seq=Pdl_Ordseq And Ordsb_Id=Pdl_Id
                                        Inner Join Customer On Cu_No=Ord_Cust And Cu_Id=Pdl_Id
                                        Inner Join Machine On Mch_No=Pdl_MchNo And Mch_Id=Pdl_Id
                                        Left Outer Join fdata f1 On f1.Fld_Id=N'Box_GoodsType' And f1.Fld_Dataid=Ordsb_GoodsType
                                        Left Outer Join fdata f2 On f2.Fld_Id=N'Box_WipType' And f2.Fld_Dataid=Ordsb_WipType
                                        Left Outer Join (
                                            Select Brc_Order, Brc_OrdSeq, Brc_SchId, Brc_Id, Sum(Brc_Qty) as Brc_Qty
                                            From BarcodePkg
                                            Where Brc_Status in ('0','6') And Brc_StkTime Is Null And Brc_PalletNo<>''
                                            Group By Brc_Order, Brc_OrdSeq, Brc_SchId, Brc_Id
                                        ) as m1 On Brc_Order=PdL_Order And Brc_OrdSeq=PdL_OrdSeq And Brc_SchId=PdL_SchId And Brc_Id=PdL_Id
                                        {baseWhereClause} {searchFilterClause}
                                    ) AS PagedResults
                                    WHERE RowNum BETWEEN @start + 1 AND @start + @length
                                    ORDER BY pdl_Date DESC;";

                    using (var cmd = new SqlCommand(dataQuery, conn))
                    {                        
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }

                        cmd.Parameters.AddWithValue("@length", length);
                        cmd.Parameters.AddWithValue("@start", start);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) //Cu_Na, Pdl_Order, Ordsb_Boxname, Pdl_Boxno, Mch_Name, Pdl_Mchno, Pdl_Trgqty, Ordsb_Qty, Ordsb_Ordsize, Pdl_Date, pdl_Adduser
                            {
                                ScheduledItems.Add(new MacScheduleItem
                                {
                                    cu_Na = reader.GetString(reader.GetOrdinal("cu_Na")),                                    
                                    pdl_Order = reader.GetString(reader.GetOrdinal("pdl_Order")),
                                    ordsb_Boxname = reader.GetString(reader.GetOrdinal("ordsb_Boxname")),
                                    pdl_Boxno = reader.IsDBNull(reader.GetOrdinal("pdl_Boxno")) ? null : reader.GetString(reader.GetOrdinal("Pdl_Boxno")),
                                    mch_Name = reader.IsDBNull(reader.GetOrdinal("mch_Name")) ? null : reader.GetString(reader.GetOrdinal("Mch_Name")),
                                    pdl_Mchno = reader.GetString(reader.GetOrdinal("pdl_Mchno")),
                                    pdl_Trgqty = reader.GetInt32(reader.GetOrdinal("pdl_Trgqty")),
                                    ordsb_Qty = reader.GetInt32(reader.GetOrdinal("ordsb_Qty")),
                                    ordsb_Ordsize = reader.IsDBNull(reader.GetOrdinal("ordsb_Ordsize")) ? null : reader.GetString(reader.GetOrdinal("Ordsb_Ordsize")),
                                    pdl_Date = reader.GetString(reader.GetOrdinal("pdl_Date")),
                                    pdl_Adduser = reader.GetString(reader.GetOrdinal("pdl_Adduser"))                        
                                });                                
                            }
                        }
                    }
                }

                var response = new DataTableResponse
                {
                    Draw = draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = ScheduledItems
                };

                return JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                return JsonSerializer.Serialize(new { error = "Error loading data: " + ex.Message, draw = draw }); // Include draw for DataTables
            }
        }
    }
}
