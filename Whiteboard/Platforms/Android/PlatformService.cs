using Android.OS;

namespace Com.Gitusme.Whiteboard.Platforms
{
    public partial class PlatformService
    {
        /// <summary>
        /// 获取存储路径
        /// </summary>
        /// <returns></returns>
        public partial string GetStoragePath()
        {
            return Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures).AbsolutePath;
        }
    }
}
