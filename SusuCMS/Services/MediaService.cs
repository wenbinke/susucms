using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using SusuCMS.Data;

namespace SusuCMS.Services
{
    public  class MediaService
    {
        public static IList<Media> GetList(int siteId, string path)
        {
            var list = new List<Media>();
            var newPath = GetRootFolderAbsolutePath(siteId);
            if (!String.IsNullOrWhiteSpace(path))
            {
                newPath = Path.Combine(newPath, path);
            }

            var directoryInfo = new DirectoryInfo(newPath);
            foreach (var item in directoryInfo.GetFiles())
            {
                list.Add(new Media
                {
                    CreationTime = item.CreationTime,
                    Name = item.Name,
                    Size = item.Length,
                    Type = Path.GetExtension(item.Name),
                    LastWriteTime = item.LastWriteTime,
                    Path = GetMediaPath(path, item.Name)
                });
            }

            foreach (var item in directoryInfo.GetDirectories())
            {
                list.Add(new Media
                {
                    Name = item.Name,
                    Size = GetDirectorySize(item),
                    IsFolder = true,
                    CreationTime = item.CreationTime,
                    LastWriteTime = item.LastWriteTime,
                    Path = GetMediaPath(path, item.Name)
                });
            }

            return list;
        }

        public static void CreateFolder(int siteId, string path, string name)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                Directory.CreateDirectory(Path.Combine(GetRootFolderAbsolutePath(siteId), name));
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(GetRootFolderAbsolutePath(siteId), path, name));
            }
        }

        private static string GetMediaPath(string path, string name)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return name;
            }

            return path + "/" + name;
        }

        public static string GetRootFolderAbsolutePath(int siteId)
        {
            var folder = HttpContext.Current.Server.MapPath(string.Format("~/Cms_Data/{0}", siteId));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }

        private static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            var fileInfos = directoryInfo.GetFiles();
            foreach (var item in fileInfos)
            {
                if (!IsHidden(item))
                {
                    size += item.Length;
                }
            }

            var directoryInfos = directoryInfo.GetDirectories();
            foreach (var item in directoryInfos)
            {
                if (!IsHidden(item))
                {
                    size += GetDirectorySize(item);
                }
            }

            return size;
        }

        private static bool IsHidden(FileSystemInfo info)
        {
            return (info.Attributes & FileAttributes.Hidden) != 0;
        }
    }
}
