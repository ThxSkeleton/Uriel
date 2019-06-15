namespace Uriel.DataTypes
{
    public abstract class PrettyPrintObject
    {
        public override string ToString()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return javaScriptSerializer.Serialize(this);
        }
    }
}
