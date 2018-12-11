using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.MetaDescriptions.Dto
{
    // MetaDescription
    public class MetaDescriptionInput : UpdateTokenModel<int>
    {
        [Required(ErrorMessage = "LBL_METADESCRIPTION_Alias_REQUIRED")]
        public string Alias { get; set; }
        [Required(ErrorMessage = "LBL_METADESCRIPTION_Name_REQUIRED")]
        public string Name { get; set; }
        [Required(ErrorMessage = "LBL_METADESCRIPTION_Content_REQUIRED")]
        public string Content { get; set; }
    }
}
