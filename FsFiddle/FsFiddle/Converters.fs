namespace Converters

open Models
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
        
type LazyTextConverter() =
    interface IValueConverter with
        member x.Convert(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            match value with
            | :? Lazy<ResponseBody> as body ->
                match body with
                | Lazy(Text text) ->
                    upcast text
                | _ -> null
            | _ -> null
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        
type LazyBinaryConverter() =
    interface IValueConverter with
        member x.Convert(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            match value with
            | :? Lazy<ResponseBody> as body ->
                match body with
                | Lazy(Image binary)
                | Lazy(Video binary) ->
                    upcast binary
                | _ -> null
            | _ -> null
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        