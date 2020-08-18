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

let setData =
    let rng = System.Random(123)
    [|for _ in 0..numberOfSets-1 do
        [|for _ in 1..setSize -> (rng.Next(0, populationSize))|]
        |> Set.ofArray
    |]
let keySetData = setData |> Array.map KeySet

module Set =

    let unionTest () =
        let rng = System.Random(123)
        let mutable result = Set.empty

        for i in 0..numberIterations-1 do
            result <- setData.[rng.Next(0, numberOfSets-1)] + setData.[rng.Next(0, numberOfSets-1)]

        result.Count

    let intersectTest () =
        let rng = System.Random(123)
        let mutable result = Set.empty

        for i in 0..numberIterations-1 do
            result <- Set.intersect setData.[rng.Next(0, numberOfSets-1)] setData.[rng.Next(0, numberOfSets-1)]

        result.Count

module KeySet =


    let unionTest () =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for i in 0..numberIterations-1 do
            result <- keySetData.[rng.Next(0, numberOfSets-1)] + keySetData.[rng.Next(0, numberOfSets-1)]

        result.Values.Length

    let intersectTest () =
        let rng = System.Random(123)
        let mutable result = KeySet (Set.empty)

        for i in 0..numberIterations-1 do
            result <- KeySet.intersect keySetData.[rng.Next(0, numberOfSets-1)] keySetData.[rng.Next(0, numberOfSets-1)]

        result.Values.Length
