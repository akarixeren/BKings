using System;
using System.IO;

namespace BikeKings_System.Helpers
{
    public static class ProductImageHelper
    {
        // High-res, authentic motorcycle part photos
        public const string DefaultTireImage = "https://images.unsplash.com/photo-1578844251758-2f71da64c96f?w=400&auto=format&fit=crop&q=80";
        public const string DefaultOilImage = "https://images.unsplash.com/photo-1619642751034-765dfdf7c58e?w=400&auto=format&fit=crop&q=80";
        public const string DefaultSparkImage = "https://images.unsplash.com/photo-1486006920555-c77dce18193b?w=400&auto=format&fit=crop&q=80";
        public const string DefaultBrakeImage = "https://images.unsplash.com/photo-1600705722908-bab1e61c0b4d?w=400&auto=format&fit=crop&q=80";
        public const string DefaultBeltImage = "https://images.unsplash.com/photo-1558981403-c5f9899a28bc?w=400&auto=format&fit=crop&q=80";
        public const string DefaultPartImage = "https://images.unsplash.com/photo-1568772585407-9361f9bf3a87?w=400&auto=format&fit=crop&q=80";

        /// <summary>
        /// Resolves an image path to a valid web URL or absolute local file path.
        /// If missing or empty, automatically provides a category-matching photo.
        /// </summary>
        public static string ResolveImagePath(string imagePath, string productName = "", string categoryName = "")
        {
            // 1. Direct Web URL (HTTP/HTTPS)
            if (!string.IsNullOrWhiteSpace(imagePath) &&
               (imagePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                imagePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
            {
                return imagePath;
            }

            // 2. Direct local file on disk - MUST return absolute full path for WPF bindings
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                try
                {
                    // If already a full rooted path and exists
                    if (Path.IsPathRooted(imagePath) && File.Exists(imagePath))
                        return Path.GetFullPath(imagePath);

                    // Check relative to current working directory
                    if (File.Exists(imagePath))
                        return Path.GetFullPath(imagePath);

                    // Check relative to application directory (e.g., bin\Debug\...\ServiceImages\...)
                    string appLocal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
                    if (File.Exists(appLocal))
                        return Path.GetFullPath(appLocal);

                    // Check in ServiceImages folder specifically
                    string serviceFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceImages", Path.GetFileName(imagePath));
                    if (File.Exists(serviceFolder))
                        return Path.GetFullPath(serviceFolder);

                    // Check in Images\Products folder specifically
                    string productFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Products", Path.GetFileName(imagePath));
                    if (File.Exists(productFolder))
                        return Path.GetFullPath(productFolder);
                }
                catch { }
            }

            // 3. Fallback: Smart Motorcycle Category Photo
            string text = $"{productName} {categoryName}".ToLower();

            if (text.Contains("tire") || text.Contains("tubeless") || text.Contains("tube") || text.Contains("victra") || text.Contains("zevo") || text.Contains("maxxis") || text.Contains("dunlop") || text.Contains("fdr"))
                return DefaultTireImage;

            if (text.Contains("oil") || text.Contains("lubricant") || text.Contains("coolant") || text.Contains("fluid") || text.Contains("motul") || text.Contains("yamalube") || text.Contains("castrol"))
                return DefaultOilImage;

            if (text.Contains("spark") || text.Contains("battery") || text.Contains("ignition") || text.Contains("ngk") || text.Contains("yuasa") || text.Contains("plug"))
                return DefaultSparkImage;

            if (text.Contains("brake") || text.Contains("pad") || text.Contains("rotor") || text.Contains("brembo") || text.Contains("shoe") || text.Contains("caliper"))
                return DefaultBrakeImage;

            if (text.Contains("belt") || text.Contains("pulley") || text.Contains("spring") || text.Contains("flyball") || text.Contains("cvt") || text.Contains("clutch") || text.Contains("jvt") || text.Contains("bando"))
                return DefaultBeltImage;

            return DefaultPartImage;
        }

        public static string GetCategoryIcon(string name, string categoryOrType = "")
        {
            return "🔧";
        }
    }
}