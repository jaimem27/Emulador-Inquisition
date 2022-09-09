namespace Stump.Server.WorldServer.Database.Items.Prism {
    public class PrismNpcItemRelator {
        public static string FetchQuery = "SELECT * FROM prism_items";
        public static string FetchByOwner = "SELECT * FROM prism_items WHERE OwnerId={0}";
    }
}