namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query) 
    {
        //Convirtiendo el query en una lista de palabras a buscar y en otra lista de palabras con operadores
        string[] SeparatedQuery = Methods.StrConverter(query);
        string[] OperatorQuery = Methods.StrConverterOperatorVersion(query);


        //Creando 4 listas de palabras, cada lista se refiere a un operador que la modifica
        string[] NoSign = Operators.NoSign(OperatorQuery);
        string[] YesSign = Operators.YesSign(OperatorQuery);
        string[] CloseSign = Operators.CloseSign(OperatorQuery);
        string[] CareSign = Operators.CareSign(OperatorQuery);

        //Creando la lista interseccion entre el query y las palabras y la lista de recomendaciones
        List<string> IntersectionWords = Methods.Intersection(SeparatedQuery,WordsProcessing.wordsvector);
        string[] Recommendations = Methods.ClosestAnswer(SeparatedQuery,WordsProcessing.wordsvector);


        //Creando la lista de documentos que contienen alguna palabra del query
        List<TXT> IntersectionDocs = Methods.TxtObtainer(SeparatedQuery,FilesProcessing.TXTvector);
        List<(TXT,float)> Response = Methods.CosSimilitude(query, IntersectionDocs, ScoreProcess.ScoreMatrix, IntersectionWords, Dictionary.dicWords, Dictionary.dicTXT);
        

        //Aplicando trabajo con operadores
        Response = Operators.OpNo(Response, NoSign);
        Response = Operators.OpYes(Response, YesSign);
        Response = Operators.OpCare(Response, CareSign);
        Response = Operators.OpClose(Response, CloseSign);


        //Transformando elementos en SearchItems (y hallando el Snippet)
        SearchItem[] items = Methods.ToSearchItem(Response, ScoreProcess.ScoreMatrix, IntersectionWords, Dictionary.dicWords, Dictionary.dicTXT);
        Array.Sort(items, (x,y) => y.Score.CompareTo (x.Score));
        items = Methods.EliminateDouble(items);


        //Si la búsqueda está vacía, retorna una búsqueda fallida
        if(items.Length==0)
        {
            items = new SearchItem[]{new SearchItem("Búsqueda Fallida","No hemos encontrado contenido relacionado a su búsqueda, intente ser más específico",1.0f)};
        }
        if(String.Join(" ", Recommendations)==query)
        {
            return new SearchResult(items, "");
        }
        return new SearchResult(items, String.Join(" ", Recommendations));
    }
}
