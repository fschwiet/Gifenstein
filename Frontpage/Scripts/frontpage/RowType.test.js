
describe("AllRightAnnouncement", function () {

    it("can be converted to a DTO", function () {
        var sut = new AllRightAnnouncement();

        expect(sut.ToDTO()).toEqual({
            type: "AllRightAnnouncement",
            announcement: "We're going to master the gif technology."
        });
    });
});

describe("AllRightMinor", function () {

    it("can be converted to a DTO", function () {
        var sut = new AllRightMinor();

        expect(sut.ToDTO()).toEqual({
            type: "AllRightMinor",
            customUrl: "http://i2.kym-cdn.com/photos/images/masonry/000/306/930/d20.gif"
        });
    });
});

describe("AllRightMajor", function () {

    it("can be converted to a DTO", function () {
        var sut = new AllRightMajor();

        expect(sut.ToDTO()).toEqual({
            type: "AllRightMajor",
            customUrl: "http://i1.kym-cdn.com/photos/images/masonry/000/422/365/4ef.gif"
        });
    });
});