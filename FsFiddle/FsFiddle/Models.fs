namespace Models

type Request =
    { Header: Map<string, string>
      Body: Lazy<string> }
    static member Default = { Header = Map.empty; Body = lazy("") }

type ResponseBody =
    | Text of string
    | Image of byte[]
    | Video of byte[]

type Response =
    { Code: int
      MimeType: string
      Header: Map<string, string>
      Body: Lazy<ResponseBody> }
    static member Default = { Code = 200; MimeType = "text/plain"; Header = Map.empty; Body = lazy(Text "") }

type Capture =
    { Url: string
      Request: Request
      Response: Response }
    static member Default =
        { Url = ""; Request = Request.Default; Response = Response.Default }
