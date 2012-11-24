

describe("RowTypeManager", function() {

    it("Can be used to navigate between the types", function () {

        var rowTypeManager = new RowTypeManager();
        rowTypeManager.addRowType("a");
        rowTypeManager.addRowType("b");
        rowTypeManager.addRowType("c");
        rowTypeManager.addRowType("d");

        expect(rowTypeManager.GetAdjacent("b")).toEqual(["a", "c"]);
        expect(rowTypeManager.GetAdjacent("a")).toEqual(["d", "b"]);
        expect(rowTypeManager.GetAdjacent("d")).toEqual(["c", "a"]);
    });
});
    
    
