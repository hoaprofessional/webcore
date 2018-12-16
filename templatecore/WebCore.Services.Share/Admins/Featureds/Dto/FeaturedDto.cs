﻿using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Featureds.Dto
{
    // Featured
    public class FeaturedDto : UpdateTokenModel<int>
    {
        public string Text { get; set; }
        public string NoValidateText { get; set; }
        public string Between10To20Character { get; set; }
        public int? Number { get; set; }
        public int? Beetween10To20 { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Money { get; set; }
        public string Combobox { get; set; }
        public string MultiSelect { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? LessThanToday { get; set; }
        public DateTime? GreaterThanToday { get; set; }
        public string TextArea { get; set; }
        public string TextEditor { get; set; }

        public string Image { get; set; }
        public string MultiImage { get; set; }
        public string File { get; set; }
        public string MultiFile { get; set; }
        public long? RecordStatus { get; set; }
    }
}
