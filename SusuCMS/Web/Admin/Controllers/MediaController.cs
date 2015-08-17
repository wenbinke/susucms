using System.Web.Mvc;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using ICSharpCode.SharpZipLib.Zip;
using System.Web;
using SusuCMS.Web.Admin.Models;
using SusuCMS.Data.Enums;
using SusuCMS.Services;


namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Media)]
    public class MediaController : AdminControllerBase
    {
        public ActionResult Index(string path)
        {
            var list = MediaService.GetList(CurrentSiteId, path);

            return View(list);
        }

        public ActionResult CreateFolder(string path)
        {
            return View(new FolderModel { Path = path, IsCreate = true });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateFolder(FolderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MediaService.CreateFolder(CurrentSiteId, model.Path, model.Name);
                    ShowSuccess(MessageResource.CreateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(MessageResource.CreateFailed);
                }
            }

            return View(model);
        }

        public ActionResult FolderAvailable(string name, string path, bool isCreate)
        {
            var rootFolder = MediaService.GetRootFolderAbsolutePath(CurrentSiteId);
            string newPath;
            if (isCreate)
            {
                if (String.IsNullOrWhiteSpace(path))
                {
                    newPath = Path.Combine(rootFolder, name);
                }
                else
                {
                    newPath = Path.Combine(rootFolder, path, name);
                }
            }
            else
            {
                newPath = Path.Combine(rootFolder, Path.Combine(Path.GetDirectoryName(path), name));
                if (string.Equals(Path.Combine(rootFolder, path), newPath, StringComparison.OrdinalIgnoreCase))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(!System.IO.Directory.Exists(newPath), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditFolder(string name, string path)
        {
            return View(new FolderModel { Name = name, Path = path });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditFolder(FolderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newPath = Path.Combine(Path.GetDirectoryName(model.Path), model.Name);
                    if (!string.Equals(model.Path, newPath, StringComparison.OrdinalIgnoreCase))
                    {
                        var folder = MediaService.GetRootFolderAbsolutePath(CurrentSiteId);
                        System.IO.Directory.Move(Path.Combine(folder, model.Path), Path.Combine(folder, newPath));
                    }

                    ShowSuccess(MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(MessageResource.UpdateFailed);
                }
            }

            return View(model);
        }

        public ActionResult Upload(string path)
        {
            return View(new { Path = path });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Upload(string path, HttpPostedFileBase file, bool extractZip)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var folderPath = MediaService.GetRootFolderAbsolutePath(CurrentSiteId);
                    if (!String.IsNullOrWhiteSpace(path))
                    {
                        folderPath = Path.Combine(folderPath, path);
                    }

                    if (extractZip && IsZipFile(Path.GetExtension(file.FileName)))
                    {
                        UnzipMediaFileArchive(folderPath, file.InputStream);
                    }
                    else
                    {
                        file.SaveAs(Path.Combine(folderPath, file.FileName));
                    }

                    ShowSuccess(MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
            }
            catch
            {
                ShowError(MessageResource.UpdateSuccess);
            }

            return View(new { Path = path, ExtractZip = extractZip });
        }

        protected void UnzipMediaFileArchive(string targetFolder, Stream zipStream)
        {
            var fileInflater = new ZipInputStream(zipStream);
            ZipEntry entry;
            while ((entry = fileInflater.GetNextEntry()) != null)
            {
                if (!entry.IsDirectory && !String.IsNullOrWhiteSpace(entry.Name))
                {
                    var file = CreateFile(Path.Combine(targetFolder, entry.Name));
                    using (var outputStream = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite))
                    {
                        var buffer = new byte[8192];
                        while (true)
                        {
                            var length = fileInflater.Read(buffer, 0, buffer.Length);
                            if (length <= 0)
                                break;
                            outputStream.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }

        public FileInfo CreateFile(string path)
        {
            var fileInfo = new FileInfo(path);

            // ensure the directory exists
            var dirName = Path.GetDirectoryName(fileInfo.FullName);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            System.IO.File.WriteAllBytes(fileInfo.FullName, new byte[0]);

            return fileInfo;
        }

        private static bool IsZipFile(string extension)
        {
            return string.Equals(extension.TrimStart('.'), "zip", StringComparison.OrdinalIgnoreCase);
        }

        public ActionResult EditFile(string name, string path)
        {
            return View(new FileModel { Name = name, Path = path });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditFile(FileModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newPath = Path.Combine(Path.GetDirectoryName(model.Path), model.Name);
                    if (!string.Equals(model.Path, newPath, StringComparison.OrdinalIgnoreCase))
                    {
                        var folder = MediaService.GetRootFolderAbsolutePath(CurrentSiteId);
                        System.IO.File.Move(Path.Combine(folder, model.Path), Path.Combine(folder, newPath));
                    }

                    ShowSuccess(MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(MessageResource.UpdateFailed);
                }
            }

            return View(model);
        }

        public ActionResult FileAvailable(string name, string path)
        {
            var newPath = Path.Combine(Path.GetDirectoryName(path), name);
            var folder = MediaService.GetRootFolderAbsolutePath(CurrentSiteId);
            if (string.Equals(Path.Combine(folder, path), newPath, StringComparison.OrdinalIgnoreCase))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(!System.IO.Directory.Exists(newPath), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(string path)
        {
            try
            {
                var folder = MediaService.GetRootFolderAbsolutePath(CurrentSiteId);
                path = Path.Combine(folder, path);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                else if (System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.Delete(path, true);
                }

                ShowSuccess(MessageResource.DeleteSuccess);
            }
            catch
            {
                ShowError(MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }
    }
}
