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


module Set =

    let genData numberOfSets populationSize setSize =
        let rng = System.Random(123)
        [|for _ in 0..numberOfSets-1 do
            [|for _ in 1..setSize -> rng.Next(0, populationSize)|]
            |> Set.ofArray
        |]

    let unionTest numberOfSets populationSize setSize =

        let data = genData numberOfSets populationSize setSize

        let mutable result = Set.empty
        for _ in 1..10_000 do
            result <- data.[rng.Next(0, numberOfSets)] + data.[rng.Next(0, numberOfSets)]

        result.Count

    let intersectTest numberOfSets populationSize setSize =
        
        let data = genData numberOfSets populationSize setSize
        
        let mutable result = Set.empty
        for _ in 1..10_000 do
            result <- Set.intersect data.[rng.Next(0, numberOfSets)] data.[rng.Next(0, numberOfSets)]

        result.Count

module KeySet =

    let genData numberOfSets populationSize setSize =
        Set.genData numberOfSets populationSize setSize
        |> Array.map KeySet
        

    let unionTest numberOfSets populationSize setSize =
        
        let data = genData numberOfSets populationSize setSize
        let mutable result = KeySet (Set.empty)

        for _ in 1..10_000 do
            result <- data.[rng.Next(0, numberOfSets)] + data.[rng.Next(0, numberOfSets)]

        result.Values.Length

    let intersectTest numberOfSets populationSize setSize =
        
        let data = genData numberOfSets populationSize setSize
        let mutable result = KeySet (Set.empty)

        for _ in 1..10_000 do
            result <- KeySet.intersect data.[rng.Next(0, numberOfSets)] data.[rng.Next(0, numberOfSets)]

        result.Values.Length
