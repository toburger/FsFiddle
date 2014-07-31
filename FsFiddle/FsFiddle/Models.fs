namespace Models

type Request =
    { Header: Map<string, string>
      Body: Lazy<string> }
    static member Default = { Header = Map.empty; Body = lazy("") }

type Response =
    { Code: int
      MimeType: string
      Header: Map<string, string>
      Body: Lazy<string> }
    static member Default = { Code = 200; MimeType = "text/plain"; Header = Map.empty; Body = lazy("") }

type Capture =
    { Url: string
      Request: Request
      Response: Response }
    static member Default =
        { Url = ""; Request = Request.Default; Response = Response.Default }
