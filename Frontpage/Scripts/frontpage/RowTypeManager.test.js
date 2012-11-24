

describe("RowTypeManager", function() {

    var rowTypeManager;

    beforeEach(function() {
        rowTypeManager = new RowTypeManager();
        rowTypeManager.addRowType("a");
        rowTypeManager.addRowType("b");
        rowTypeManager.addRowType("c");
        rowTypeManager.addRowType("d");
    });

    it("Can be used to navigate between the types", function () {


        expect(rowTypeManager.GetAdjacent("b")).toEqual(["a", "c"]);
        expect(rowTypeManager.GetAdjacent("a")).toEqual(["d", "b"]);
        expect(rowTypeManager.GetAdjacent("d")).toEqual(["c", "a"]);
    });

    it("Can indicate the first and last type", function () {
        expect(rowTypeManager.GetFirstType()).toEqual("a");
        expect(rowTypeManager.GetLastType()).toEqual("d");
    });
});
    
    
