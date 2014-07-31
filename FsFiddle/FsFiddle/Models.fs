namespace Models

type Request =
    { Header: Map<string, string>
      Body: Lazy<string> }

type ResponseBody =
    | Text of Lazy<string>
    | Image of Lazy<byte[]>
    | Video of Lazy<byte[]>

type Response =
    { Code: int
      MimeType: string
      Header: Map<string, string>
      Body: ResponseBody }

type Capture =
    { Url: string
      Request: Request
      Response: Response }
