namespace XBMCRPC.Video.Details
{
   public class File : Item
   {
       public global::System.Collections.Generic.List<string> director { get; set; }
       public Resume resume { get; set; }
       public int runtime { get; set; }
       public Streams streamdetails { get; set; }
    }
}
