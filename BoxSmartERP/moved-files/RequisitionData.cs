using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP.Services
{
    public class RequisitionData
    {
        public string ControlNumber { get; set; }
        public DateTime RequisitionDate { get; set; } // Maps to 'due_date'
        public string RequisitioningDepartment { get; set; } = "SALES / MARKETING DEPARTMENT"; // From PDF 

        // Use booleans directly from the DB query
        public bool IsRubberDie { get; set; } // Maps to 'rubberdie'
        public bool IsDieCutMould { get; set; } // Maps to 'diecut'
        public bool IsNegativeFilm { get; set; } // Maps to 'negative_film'

        public List<Customer> Customers { get; set; } = new List<Customer>();
        public bool ShowNothingFollows { get; set; } = false; // Based on the provided PDF, it's not present. Set to true if needed.

        // Signature/Approval persons - these are still placeholders as no DB fields provided for them
        public SignaturePerson PreparedBy { get; set; } = new SignaturePerson { Name = "SALES & MARKETING STAFF", Title = "" }; // From PDF 
        public SignaturePerson ReviewedBy { get; set; } = new SignaturePerson { Name = "SALES & MARKETING MANAGER/ SALES ASSIST. MANAGER", Title = "" }; // From PDF 
        public SignaturePerson RecommendingApproval { get; set; }
        public SignaturePerson ReviewedByProcurement { get; set; } = new SignaturePerson { Name = "SR. PROCUREMENT & ADMIN MANAGER", Title = "" }; // From PDF 
        public SignaturePerson ReceivedBy { get; set; }
        public SignaturePerson AssistantGeneralManager { get; set; } = new SignaturePerson { Name = "ASSISTANT GENERAL MANAGER", Title = "" }; // From PDF 


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