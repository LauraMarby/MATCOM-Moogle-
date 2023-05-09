namespace MoogleEngine;
public class TXT
{
    public string title;   
    public string text;

    public TXT (FileInfo X) 
    {
        this.title = Methods.NameFile(X.Name);
        StreamReader L = new StreamReader(X.FullName);
        this.text = L.ReadToEnd();
    }
    public TXT (string q)
    {
        this.title = "Query";
        this.text = q;
    }
}