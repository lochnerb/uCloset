//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fbuCloset.Models
{
    using System;
    
    public partial class spView_OutfitGroup_Result
    {
        public int OutfitGroupID { get; set; }
        public string OutfitGroupDesc { get; set; }
        public int OutfitGroupParentID { get; set; }
        public int OutfitGroupRank { get; set; }
        public bool OutfitGroupShow { get; set; }
        public Nullable<int> ChildCount { get; set; }
        public string HasChild { get; set; }
        public string OutfitGroupPath { get; set; }
    }
}