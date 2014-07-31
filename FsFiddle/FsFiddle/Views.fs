namespace Views

open FSharpx
open System.Windows.Controls

type ShellViewXAML = XAML<"ShellView.xaml">

type ShellView() as self =
    inherit UserControl()

    let xaml = ShellViewXAML()

    do self.Content <- xaml.Root
