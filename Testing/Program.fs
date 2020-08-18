// Learn more about F# at http://fsharp.org

open System
open KeySet


//module Set =

//    let unionTest (numberIterations) =
//        let rng = System.Random(123)
//        let mutable result = Set.empty
//        for _ in 1..numberIterations do
//            result <- setData.[rng.Next(0, numberOfSets)] + setData.[rng.Next(0, numberOfSets)]

//        result.Count

//    let intersectTest (numberIterations) =
//        let rng = System.Random(123)
//        let mutable result = Set.empty
//        for _ in 1..numberIterations do
//            result <- Set.intersect setData.[rng.Next(0, numberOfSets)] setData.[rng.Next(0, numberOfSets)]

//        result.Count

module KeySet =

    //let unionTest (numberIterations) =
    //    let mutable result = KeySet (Set.empty)

    //    for _ in 1..numberIterations do
    //        for i in 0..numberOfSets-1 do
    //            result <- keySetData.[i] + keySetData.[i]

    //    result.Values.Length

    let intersectTest (numberIterations) (data:KeySet<_>[]) =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for _ in 1..numberIterations do
            result <- KeySet.intersect data.[rng.Next(0, data.Length-1)] data.[rng.Next(0, data.Length-1)]

        result.Values.Length

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"


    let numberOfSets = 1_000
    let populationSize = 100
    let setSize = 20
    let setData =
        let rng = System.Random(123)
        [|for _ in 0..numberOfSets-1 do
            [|for _ in 1..setSize -> (rng.Next(0, populationSize))|]
            |> Set.ofArray
        |]
    
    let keySetData =
        setData
        |> Array.map KeySet
    

    //printfn "Warm up..."
    //let r = KeySet.intersectTest (1) keySetData

    printfn "Running..."
    let x = KeySet.intersectTest (100_000_000) keySetData

    printfn "%A" x

    0 // return an integer exit code
