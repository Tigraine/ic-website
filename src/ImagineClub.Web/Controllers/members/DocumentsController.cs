namespace ImagineClub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Castle.ActiveRecord;
    using Castle.MonoRail.ActiveRecordSupport;
    using Castle.MonoRail.Framework;
    using Castle.MonoRail.Framework.Helpers;
    using Models;
    using NHibernate.Criterion;

    [Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
    public class DocumentsController : MemberControllerBase
    {
        public const int PageSize = 5;

        public void List(int? page)
        {
            IList<Document> documents = Document.FindAll(Order.Desc("UploadedOn"));
            int pageNumber = page ?? 1;
            PaginationHelper.CreatePagination(documents, PageSize, pageNumber);
            PropertyBag["documents"] = documents;
        }

        public void Upload()
        {
        }

        public void Upload([DataBind("Document")] Document document, HttpPostedFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                using (new SessionScope())
                {
                    long length = uploadedFile.InputStream.Length;
                    Byte[] fileContent = new byte[length];
                    int offset = 0;

                    while (offset < fileContent.Length)
                    {
                        offset += uploadedFile.InputStream.Read(fileContent, 0, (int)length - offset);
                    }

                    string fileName = uploadedFile.FileName;
                    document.BinaryFile = new BinaryDocument {BinaryData = fileContent, MimeType = FindMimeType(fileName)};
                    document.FileSize = fileContent.Length;
                    document.UploadedOn = DateTime.Now;
                    document.FileName = uploadedFile.FileName;
                    document.Uploader = (Member) Context.CurrentUser;
                    document.SaveAndFlush();

                    Flash["filename"] = document.FileName;
                    RedirectToAction("Thanks");
                }
            }
        }

        public void Thanks()
        {
        }

        public void Detail([ARFetch("id", Required = true)] Document document)
        {
            PropertyBag["document"] = document;
        }


        /// <summary>
        /// Finds the MineType for a file engine.
        /// </summary>
        /// <param name="fileName">The fileName.</param>
        /// <returns></returns>
        private string FindMimeType(string fileName)
        {
            if (fileName.EndsWith("jpg")) return "image/jpeg";
            if (fileName.EndsWith("jpg")) return "image/jpeg";
            if (fileName.EndsWith("png")) return "image/png";
            if (fileName.EndsWith("pdf")) return "application/pdf";
            if (fileName.EndsWith("doc")) return "application/msword";
            if (fileName.EndsWith("docx")) return "application/msword";
            if (fileName.EndsWith("xls")) return "application/vnd.ms-excel";
            if (fileName.EndsWith("xlsx")) return "application/vnd.ms-excel";
            if (fileName.EndsWith("zip")) return "application/zip";

            return "application/octet-stream";
        }

        public void Get([ARFetch("id")] Document document)
        {
            Context.Response.ContentType = document.BinaryFile.MimeType;
            Context.Response.BinaryWrite(document.BinaryFile.BinaryData);
            string encodedFilename = new HtmlHelper(Context).UrlEncode(document.FileName);
            Context.Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", encodedFilename));
            CancelLayout();
            CancelView();
            return;
        }
    }
}