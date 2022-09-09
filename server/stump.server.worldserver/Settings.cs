
using System.Drawing;
using Stump.Core.Attributes;

namespace Stump.Server.WorldServer
{
    /// <summary>
    ///   Global settings defined by the config file
    /// </summary>
    public class Settings
    {
        [Variable(true)]
        public static string MOTD = "Bienvenido a Inquisition <b>Stump v. pre-alpha by Shine</b>";

        private static string m_htmlMOTDColor = ColorTranslator.ToHtml(Color.OrangeRed);
        private static Color m_MOTDColor = Color.OrangeRed;

        [Variable(true)]
        public static string HtmlMOTDColor
        {
            get { return m_htmlMOTDColor; }
            set
            {
                m_htmlMOTDColor = value;
                m_MOTDColor = ColorTranslator.FromHtml(value);
            }
        }

        public static Color MOTDColor
        {
            get { return m_MOTDColor; }
            set
            {
                m_htmlMOTDColor = ColorTranslator.ToHtml(value);
                m_MOTDColor = value;
            }
        }
    }
}