// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public readonly struct Ratchet
{
    private Ratchet(Offset trigger, Offset moveStopTo)
    {
        Trigger = trigger;
        MoveStopTo = moveStopTo;
    }

    public Offset Trigger { get; init; }
    public Offset MoveStopTo { get; init; }

    public override string ToString() => 
        $"{Trigger} => {MoveStopTo}";

    public static Ratchet From(Offset trigger, Offset moveStopTo)
    {
        trigger.MayNot().BeDefault();
        moveStopTo.MayNot().BeDefault();

        return new Ratchet(trigger, moveStopTo);
    }
}