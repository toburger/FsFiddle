namespace Models

type Request =
    { Header: Map<string, string>
      Body: Lazy<string> }
    static member Default = { Header = Map.empty; Body = lazy("") }

type ResponseBody =
    | Text of Lazy<string>
    | Image of Lazy<byte[]>
    | Video of Lazy<byte[]>

type Response =
    { Code: int
      MimeType: string
      Header: Map<string, string>
      Body: ResponseBody }
    static member Default = { Code = 200; MimeType = "text/plain"; Header = Map.empty; Body = Text (lazy("")) }

type Capture =
    { Url: string
      Request: Request
      Response: Response }
    static member Default =
        { Url = ""; Request = Request.Default; Response = Response.Default }
