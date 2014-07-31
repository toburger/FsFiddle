namespace Converters

open System.Windows.Data

type MapConverter() =
    interface IValueConverter with
        member x.Convert(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            match value with
            | :? Map<string, string> as map ->
                map
                |> Seq.map (fun (KeyValue(k, v)) -> sprintf "%s: %s" k v)
                |> String.concat "\r\n"
                |> box
            | _ -> null
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        
type LazyConverter() =
    interface IValueConverter with
        member x.Convert(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            match value with
            | :? Lazy<string> as lzy ->
                upcast sprintf "%s" (lzy.Force())
            | _ -> null
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        
