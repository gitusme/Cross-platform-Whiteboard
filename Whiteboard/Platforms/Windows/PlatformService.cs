
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
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }
}
