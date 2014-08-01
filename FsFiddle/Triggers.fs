namespace Triggers

open System
open System.Windows.Interactivity
open System.Windows
open System.Windows.Input

type InputBindingTrigger() =
    inherit TriggerBase<FrameworkElement>()

    let canExecuteChangedEvent = Event<_,_>()

    static let InputBindingProperty =
        DependencyProperty.Register("InputBinding",
                                    typeof<InputBinding>,
                                    typeof<InputBindingTrigger>,
                                    UIPropertyMetadata(null))

    member self.InputBinding
        with get() = self.GetValue(InputBindingProperty) :?> InputBinding
        and set(v) = self.SetValue(InputBindingProperty, (v : InputBinding))

    override self.OnAttached() =
        if self.InputBinding <> null then
            self.InputBinding.Command <- self
            self.AssociatedObject.InputBindings.Add(self.InputBinding) |> ignore
        base.OnAttached()

    interface ICommand with
        member x.CanExecute(parameter: obj): bool = 
            true
        
        [<CLIEvent>]
        member x.CanExecuteChanged: IEvent<_, _> = 
            canExecuteChangedEvent.Publish
        
        member x.Execute(parameter: obj): unit = 
            x.InvokeActions(parameter)
        