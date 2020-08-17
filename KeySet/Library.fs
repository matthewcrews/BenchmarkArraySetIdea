namespace KeySet

open System

type KeySet<'a when 'a : comparison>(values:Memory<'a>) =

    member this.Values = values

    new(values:Set<'a>) =
        let v = Set.toArray values
        KeySet(v.AsMemory())

    static member findIndexOf startingLowerBound x (values:Memory<'a>) =
        let mutable lowerBound = startingLowerBound
        let mutable upperBound = values.Length - 1
        let mutable idx = (lowerBound + upperBound) / 2

        while lowerBound <= upperBound do
            if values.Span.[idx] <= x then
                lowerBound <- idx + 1
                idx <- (lowerBound + upperBound) / 2
            elif values.Span.[idx] > x then
                upperBound <- idx - 1
                idx <- (lowerBound + upperBound) / 2
            else
                ()

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
            let idx = KeySet.findIndexOf 0 x this.Values

            KeySet (this.Values.Slice(idx + 1))

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

        KeySet(newValues.AsMemory(0, outIdx))

    static member intersect (a:KeySet<'a>) (b:KeySet<'a>) =
        let intersectAux (small:KeySet<'a>) (large:KeySet<'a>) =

          let newValues = Array.zeroCreate(small.Values.Length)

          let mutable smallIdx = 0
          let mutable largeIdx = 0
          let mutable outIdx = 0

          while (smallIdx < small.Values.Length) do
              largeIdx <- KeySet.findIndexOf largeIdx (small.Values.Span.[smallIdx]) large.Values

              if small.Values.Span.[smallIdx] = large.Values.Span.[largeIdx] then
                  newValues.[outIdx] <- small.Values.Span.[smallIdx]
                  outIdx <- outIdx + 1

              smallIdx <- smallIdx + 1

          KeySet(newValues.AsMemory().Slice(0, outIdx))

        if a.Values.Length < b.Values.Length then
          intersectAux a b
        else
          intersectAux b a
