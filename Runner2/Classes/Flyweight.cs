using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Runner2.Classes
{
    /// <summary>
    /// Flyweight/concrete flyweight
    /// </summary>
	public class Icon
	{
		public ImageBrush brush;

		public Icon(string path)
		{
			brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(path));
		}
	}

	
    /// <summary>
    /// Flyweigth factory
    /// </summary>
	public class IconProvider
	{
		private static Dictionary<string, Icon> icons = new Dictionary<string, Icon>();

        public Icon GetIcon(string key)
        {
            // Uses "lazy initialization"
            Icon icon = null;
            if (icons.ContainsKey(key))
            {
                icon = icons[key];
            }
            else
            {
                switch (key)
                {
                    case "summer" : icon = new Icon("pack://application:,,,/Images/screen-1.jpg"); break;
                    case "winter" : icon = new Icon("pack://application:,,,/Images/screen-12.jpg"); break;
                }
                icons.Add(key, icon);
            }
            return icon;
        }
    }
}
