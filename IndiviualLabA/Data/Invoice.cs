using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IndiviualLabA.Data
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Invoice ID")]
        public int InvoiceID { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Date")]
        public DateTime Created { get; set; }
        [Required]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        // Parent.
        public virtual CustomUser CustomUser { get; set; }
    }



}
