//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcLibrary.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Announcement
    {
        public byte AnnouncementId { get; set; }
        public string AnnouncementCategory { get; set; }
        public string AnnouncementContent { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
