//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DenemeSWE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            this.Shelf = new HashSet<Shelf>();
        }
    
        public double F1 { get; set; }
        public string book_id { get; set; }
        public string photo { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string language { get; set; }
        public string subject { get; set; }
        public string category { get; set; }
        public string release_date { get; set; }
        public string reading_link { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shelf> Shelf { get; set; }
    }
}
