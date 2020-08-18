namespace KeySet

open System
open System.Collections.Generic

type KeySet<[<EqualityConditionalOn>]'a when 'a : comparison>(comparer:IComparer<'a>, values:Memory<'a>) =
    member this.Comparer = comparer
    member this.Values = values

    new(values:Set<'a>) =
        let comparer = LanguagePrimitives.FastGenericComparer<'a>
        let v = Set.toArray values
        KeySet(comparer, v.AsMemory<'a>())

    static member inline findIndexOf (comparer:IComparer<'a>) startingLowerBound x (values:Memory<'a>) =
        let mutable lowerBound = startingLowerBound
        let mutable upperBound = values.Length - 1
        let mutable idx = (lowerBound + upperBound) / 2

        while lowerBound <= upperBound do
            let x = comparer.Compare(values.Span.[idx], x)
            if x <= 0 then
                lowerBound <- idx + 1
                idx <- (lowerBound + upperBound) / 2
            else
                upperBound <- idx - 1
                idx <- (lowerBound + upperBound) / 2

        idx

    member this.GreaterThan x =

        if this.Values.IsEmpty then
            this
        elif this.Values.Length = 1 then
            if this.Values.Span.[0] > x then
                this
            else
                KeySet(Set.empty)
        else
            let idx = KeySet.findIndexOf this.Comparer 0 x values

            KeySet (comparer, values.Slice(idx + 1))

    static member inline (+) (a:KeySet<'a>, b:KeySet<'a>) =
        let newValues = Array.zeroCreate(a.Values.Length + b.Values.Length)

        let mutable aIdx = 0
        let mutable bIdx = 0
        let mutable outIdx = 0

        while (aIdx < a.Values.Length && bIdx < b.Values.Length) do
            
            if a.Values.Span.[aIdx] < b.Values.Span.[bIdx] then
                newValues.[outIdx] <- a.Values.Span.[aIdx]
                aIdx <- aIdx + 1
                outIdx <- outIdx + 1
            elif a.Values.Span.[aIdx] > b.Values.Span.[bIdx] then
                newValues.[outIdx] <- b.Values.Span.[bIdx]
                bIdx <- bIdx + 1
                outIdx <- outIdx + 1
            else
                newValues.[outIdx] <- a.Values.Span.[aIdx]
                aIdx <- aIdx + 1
                bIdx <- bIdx + 1
                outIdx <- outIdx + 1

        while aIdx < a.Values.Length do
            newValues.[outIdx] <- a.Values.Span.[aIdx]
            aIdx <- aIdx + 1
            outIdx <- outIdx + 1

        while bIdx < b.Values.Length do
            newValues.[outIdx] <- b.Values.Span.[bIdx]
            bIdx <- bIdx + 1
            outIdx <- outIdx + 1

        KeySet(a.Comparer, newValues.AsMemory(0, outIdx))

    static member inline intersect (a:KeySet<'a>) (b:KeySet<'a>) =
        let intersectAux (small:KeySet<'a>) (large:KeySet<'a>) =

          let newValues = Array.zeroCreate(small.Values.Length)

          let mutable smallIdx = 0
          let mutable largeLowerIdx = 0
          let mutable outIdx = 0

          while (smallIdx < small.Values.Length) do
              largeLowerIdx <- KeySet.findIndexOf a.Comparer largeLowerIdx (small.Values.Span.[smallIdx]) large.Values

              let x = a.Comparer.Compare(small.Values.Span.[smallIdx], large.Values.Span.[largeLowerIdx])
              if x = 0 then
                  newValues.[outIdx] <- small.Values.Span.[smallIdx]
                  outIdx <- outIdx + 1

              smallIdx <- smallIdx + 1

          KeySet(a.Comparer, newValues.AsMemory().Slice(0, outIdx))

        if a.Values.Length < b.Values.Length then
          intersectAux  a b
        else
          intersectAux b a
