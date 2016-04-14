//Script by Robert Coleman
var x = 5;
//var val1 = (x == "5"); // x is equal to value of 5 - true type coercion - turns into same type
var val1 = x === "5"; // x is equal to type of 5 - false - does not perform type coercion - must both be string or int for true
//NaN == NaN - returns false
//nul == undefined returns true
//Logical operators - chain together boolean logic
    // && - and     (x < 10) && (y > 10)
    // || - or      (x < 10) || (y > 10)
// !  - not     !(value)
var x = 3;
var y = 10;
//val1 = y === "b" || x >= 10;
val1 = !(x == "3" || x === y) && !(y != 8 && x <= y) //Javascript short-circuiting - skips remiander of OR decision if it finds a true
console.log("result is " + val1.toString());

//Logic
/*var str = "";
var msg = "Haha!"
var isFunny = "false";
console.log(!"false");
console.log(!((str || msg) && isFunny));
var age = 19;
if (age < 18) {
    console.log("Too young to enter");
} 
else if(age <= 21){
    console.log("May enter but not drink");
}
else {
    console.log("May enter and drink")
}*/

//For and While
/*var stringtest = "Hello";
var count = 0;
while (count < stringtest.length) {
    console.log(stringtest[count]);
    count++;
}

for (var i = 1; i < 32; i += 8) {
    console.log("value " + i);
}
var str = "dfgadkglearkjglkjgfa";
for (var i = 0; i < str.length; i+=2) {
    console.log(str[i]);
}*/

//JS Functions

//declare funtion
function printValue() {
    console.log("Hello World")
}
//Function with argument
function squareNum(num) {
    console.log(num * num);
}

function logIn(userName) {
    console.log("welcome back, " + userName);
}

function calcRect(x, y) {
    console.log(x * y);
}

function calcReturn(x, y) {
    return (x * y);
}

function capitalize(str) {
    if (typeof str != "string") { // Determine if a string
        return ("Not a string");
    }
    return str.charAt(0).toUpperCase() + str.slice(1);
}

function isEven(num) {
    if (typeof num != "number") {
        return ("not a number");
    }
    else if (num % 2 == 0) {
        return ("An even number");
    }
    else {
        return ("Not an even number");
    }
}
function factorial(num) {
    if (typeof num != "number") {
        return ("not a number");
    }
    if (num == 0) {
        return (1);
    }
    var result = num
    for (count = num - 1; count > 0; count--) {
        result = result * count;
    }
    return (result);
}
function kebabToSnake(str) {
    if (typeof str != "string") {
        return ("not a string");
    }
    var result = "";
    for (i = 0; i < str.length; i++) {
        if (str[i] == "-") {
            result = result + "_";
        }
        else {
            result = result + str[i];
        }
    }
    return (result);
}

function doMath() {
    var x = 40; // x is defined within this scope, outside - not defined
    console.log(x); // Defining x outside of this scope will have no effect, since it is initialised in this scope as well
    //This will also not change x outside of this function either
}
function doOtherMath() {
    console.log(x); // x defined outside of this scope will be used in here
}
function doFinalMath() {
    x = 100;
    console.log(x);
}

//Create JScript object all at once
var person = {
    name: "Robbo",
    age: 32,
    city: "Missoula"
};
//Retrieve data:
//console.log(person["name"]);
//console.log(person.name);
//lookup value
//var str = "name"
//person[str] does not work for person.str
//update value
//person["age"] += 1;
//person["city"] = London
//Create empty JScript object first
var person = {}
person.name = "Hermione";
person.age = 23;
person.city = "Kansas City";
//Or
var person = new Object();
person.name = "Geralt";
person.age = 54;
person.city = "Rivia";

//array with objects
var posts = [
    {
        title: "Heaps important",
        author: "Rob Coleman",
        comments: ["You suck", "You Rock"]
    },
    {
        title: "Yeah baby",
        author: "Cindy",
        comments: ["Tralala", "blah"]
    }
]

var someObject = {
    friends : [
    { name : "Malfoy"},
    { name: "Crabbe" },
    { name: "Goyle"}],
    color: "Baby Blue",
    isEvil: true
}

var movies = [
    {
        title: "Lord of War",
        rating: "5 stars",
        hasWatched: true
    },
    {
        title: "The Great Gatsby",
        rating: "unknown",
        hasWatched: false
    },
    {
        title: "Hunt for Red October",
        rating: "4 stars",
        hasWatched: true
    }
]

function review(){
    var result = ""
    for (i = 0; i < movies.length; i++) {
        if (movies[i].hasWatched == true){
            result = "You have seen \"" + movies[i].title + "\" - " + movies[i].rating;
        }
        else {
            result = "You have not seen " + '"' + movies[i].title + '"' + " - " + movies[i].rating;
        }
        console.log(result);
    }
}

//methods within objects
var obj = {
    name: "Addition function",
    namespace: "console",
    add: function(x,y){
        return (x + y);
    }
}