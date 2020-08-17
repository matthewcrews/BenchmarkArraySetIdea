// Learn more about F# at http://fsharp.org

open System
open KeySet

let numberOfSets = 1_000
let populationSize = 100
let setSize = 20
let data =
    let rng = System.Random(123)
    [|for _ in 0..numberOfSets-1 do
        [|for _ in 1..setSize -> rng.Next(0, populationSize)|]
        |> Set.ofArray
    |]

module Set =

    let unionTest (numberIterations) =
        let rng = System.Random(123)
        let mutable result = Set.empty
        for _ in 1..numberIterations do
            result <- data.[rng.Next(0, numberOfSets)] + data.[rng.Next(0, numberOfSets)]

        result.Count

    let intersectTest (numberIterations) =
        let rng = System.Random(123)
        let mutable result = Set.empty
        for _ in 1..numberIterations do
            result <- Set.intersect data.[rng.Next(0, numberOfSets)] data.[rng.Next(0, numberOfSets)]

        result.Count

module KeySet =

    let data =
        data |> Array.map KeySet

    let unionTest (numberIterations) =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for _ in 1..numberIterations do
            result <- data.[rng.Next(0, numberOfSets)] + data.[rng.Next(0, numberOfSets)]

        result.Values.Length

    let intersectTest (numberIterations) =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for _ in 1..numberIterations do
            result <- KeySet.intersect data.[rng.Next(0, numberOfSets)] data.[rng.Next(0, numberOfSets)]

        result.Values.Length

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    printfn "Warm up..."
    let r = Set.intersectTest (1_000_000)

    printfn "Running..."
    let x = Set.intersectTest (100_000_000)

    printfn "%A" x

    0 // return an integer exit code
