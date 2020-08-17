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


let numberOfSets = 1_000
let populationSize = 100
let setSize = 30
let numberIterations = 50_000

module Set =

    let data =
        let rng = System.Random(123)
        [|for _ in 0..numberOfSets-1 do
            [|for _ in 1..setSize -> rng.Next(0, populationSize)|]
            |> Set.ofArray
        |]

    let unionTest () =
        let rng = System.Random(123)
        let mutable result = Set.empty
        for _ in 1..numberIterations do
            result <- data.[rng.Next(0, numberOfSets)] + data.[rng.Next(0, numberOfSets)]

        result.Count

    let intersectTest () =
        let rng = System.Random(123)
        let mutable result = Set.empty

        for _ in 1..numberIterations do
            result <- Set.intersect data.[rng.Next(0, numberOfSets)] data.[rng.Next(0, numberOfSets)]

        result.Count

module KeySet =

    let data =
        Set.data
        |> Array.map KeySet
        

    let unionTest () =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for _ in 1..numberIterations do
            result <- data.[rng.Next(0, numberOfSets)] + data.[rng.Next(0, numberOfSets)]

        result.Values.Length

    let intersectTest () =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for _ in 1..numberIterations do
            result <- KeySet.intersect data.[rng.Next(0, numberOfSets)] data.[rng.Next(0, numberOfSets)]

        result.Values.Length
