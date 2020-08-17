module KeySet.Tests

open KeySet

let rng = System.Random(123)
//let numberOfSets = 1_000
//let sizeOfSmall = 100
//let sizeOfMedium = 1_000
//let sizeOfLarge = 10_000
//let maxIndexValue = 100_000

//let randomSets =
//    [|for _ in 0..numberOfSets-1 do
//        [for _ in 1..sizeOfSmall -> rng.Next(0, maxIndexValue)]
//        |> Set.ofList
//    |]

//let randomKeySets =
//    randomSets
//    |> Array.map KeySet


let numberOfSets = 100_000
let populationSize = 100
let setSize = 30
let numberIterations = numberOfSets

module Set =

    let data =
        let rng = System.Random(123)
        [|for _ in 0..numberOfSets-1 do
            [|for _ in 1..setSize -> rng.Next(0, populationSize)|]
            |> Set.ofArray
        |]

    let unionTest () =
        let mutable result = Set.empty

        for i in 0..numberIterations-1 do
            result <- data.[i] + data.[i]

        result.Count

    let intersectTest () =
        let mutable result = Set.empty

        for i in 0..numberIterations-1 do
            result <- Set.intersect data.[i] data.[i]

        result.Count

module KeySet =

    let data =
        Set.data
        |> Array.map KeySet
        

    let unionTest () =
        let mutable result = KeySet (Set.empty)

        for i in 0..numberIterations-1 do
            result <- data.[i] + data.[i]

        result.Values.Length

    let intersectTest () =
        let mutable result = KeySet (Set.empty)

        for i in 0..numberIterations-1 do
            result <- KeySet.intersect data.[i] data.[i]

        result.Values.Length
