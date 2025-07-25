using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP.Services
{
    public class RequisitionData
    {
        public string ControlNumber { get; set; }
        public DateTime RequisitionDate { get; set; } // Maps to 'due_date' from query, displayed below table
        public string RequisitioningDepartment { get; set; } = "SALES / MARKETING DEPARTMENT"; // From PDF 

        public bool IsRubberDie { get; set; } // Maps to 'rubberdie' 
        public bool IsDieCutMould { get; set; } // Maps to 'diecut'
        public bool IsNegativeFilm { get; set; } // Maps to 'negative_film'

        public List<Customer> Customers { get; set; } = new List<Customer>();
        public bool ShowNothingFollows { get; set; } = true; // Explicitly present in this PDF 

       // Signature/Approval persons - Adjusting default names/titles based on the PDF 
        public SignaturePerson RecommendingApproval { get; set; } = new SignaturePerson { Name = "", Title = "" }; // Label is "Recommending Approval:" 
        public SignaturePerson PreparedBy { get; set; } = new SignaturePerson { Name = "SALES & MARKETING STAFF", Title = "" }; // Label is "Prepared by:" 
        public SignaturePerson ReviewedBySalesMarketing { get; set; } = new SignaturePerson { Name = "SALES & MARKETING MANAGER/\n SALES ASSIST. MANAGER", Title = "" }; // Label is "Reviewed by:" 
        public SignaturePerson ReviewedByProcurement { get; set; } = new SignaturePerson { Name = "SR. PROCUREMENT & ADMIN MANAGER", Title = "" }; // Label is "Reviewed by:" 
        public SignaturePerson ReceivedBy { get; set; } = new SignaturePerson { Name = "", Title = "" }; // Label is "Received by:" 
        public SignaturePerson AssistantGeneralManager { get; set; } = new SignaturePerson { Name = "ASSISTANT GENERAL MANAGER", Title = "" }; // No label, name is itself 

        public string DocumentCode { get; set; } = "FM-SMPC 02-175"; // From PDF 
        public string DocumentRevision { get; set; } = "Rev. 03/01"; // From PDF 
        public DateTime DocumentRevisionDate { get; set; } = new DateTime(2024, 4, 1); // From PDF 
        public class Customer
        {
            public string Name { get; set; } // Maps to co.organization_name 
            public List<RequisitionItem> Items { get; set; } = new List<RequisitionItem>();
        }

        public class RequisitionItem
        {
            public string Quantity { get; set; } // Maps to 'quantity' 
            public string Unit { get; set; }     // Maps to 'u.uom_code' 
            public string NumOuts { get; set; }  // Maps to 'tr.num_outs' - will be appended to Unit for display 
            public string Description { get; set; } // Maps to 'item_description' 
            public string Remarks { get; set; }   // Maps to 'design_notes' AND 'printcardno' 
            public string PrintcardNo { get; set; } // Maps to 'printcardno'
        }

        public class SignaturePerson
        {
            public string Name { get; set; }
            public string Title { get; set; }
        }
    }
    
}