namespace ImagineClub.Web.Models
{
    using System;
    using Castle.ActiveRecord;
    using Castle.Components.Validator;

    [ActiveRecord]
    public class Document : ValidatedActiveRecordEntity<Document>
    {
        [Property(NotNull = true)]
        [ValidateNonEmpty]
        public string Name { get; set; }

        [Property]
        [ValidateLength(0, 500)]
        public string Description { get; set; }

        [Property(NotNull = true)]
        public DateTime UploadedOn { get; set; }

        [BelongsTo(NotNull = true)]
        public Member Uploader { get; set; }

        [BelongsTo(Lazy = FetchWhen.OnInvoke, Cascade = CascadeEnum.SaveUpdate, NotNull = true)]
        public BinaryDocument BinaryFile { get; set; }

        [Property(NotNull = true)]
        public string FileName { get; set; }

        [Property(NotNull = true)]
        public long FileSize { get; set; }

        

        [Property(NotNull = true)]
        public long NumberOfDownloads { get; set; }
    }

    [ActiveRecord]
    public class BinaryDocument : ActiveRecordBase<BinaryDocument>
    {
        [PrimaryKey]
        public long Id { get; set; }

        [Property(NotNull = true)]
        public string MimeType { get; set; }

        [Property(ColumnType = "BinaryBlob", SqlType = "IMAGE", NotNull = true)]
        public byte[] BinaryData { get; set; }
    }
}