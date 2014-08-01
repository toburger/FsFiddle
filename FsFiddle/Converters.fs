namespace Converters

open Models
open System.Windows.Data
open System.Windows
open System.IO
open System.Windows.Media.Imaging

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
            | :? Lazy<string> as s -> upcast s.Force()
            | :? ResponseBody as body ->
                match body with
                | Text (Lazy(text)) ->
                    upcast text
                | _ -> null
            | _ -> null
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        
type LazyBinaryConverter() =
    interface IValueConverter with
        member x.Convert(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            match value with
            | :? ResponseBody as body ->
                match body with
                | Image (Lazy(binary))
                | Video (Lazy(binary)) ->
                    upcast binary
                | _ -> null
            | _ -> null
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        
type VisibiltyConverter() =
    interface IValueConverter with
        member x.Convert(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            match value with
            | :? bool as b ->
                if b then Visibility.Visible else Visibility.Collapsed
            | _ -> Visibility.Collapsed
            |> box
        
        member x.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            failwith "Not implemented yet"
        