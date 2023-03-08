// imports
const fs = require("node:fs");
const path = require("node:path");

// FUNCTIONS
const validateKeys = (properties) => properties.map(key => {
    let retval = "";
    for (let i = 0; i < key.length; i++)
        if (!((i === 0 || i === key.length - 1) && (key[i] === "_")))
            retval += key[i];
    return retval;
})

const reader = (path) => {
    const lines = fs.readFileSync(path)
        .toString()
        .replaceAll("\r", "")
        .replaceAll(/;[\s]?;/g, ";NULL;")
        .replaceAll("\n\n", "")
        .split("\n");
    return [
        validateKeys(lines
            .shift()
            .toLowerCase()
            .replaceAll(/[-\s]/g, "_")
            .split(";")
        ),
        lines
    ];
}

const prepare = (keys, values) => {
    const retval = {};
    if (keys.length != values.length) {
        console.log("invalid key value set length");
        console.log(keys.length, values.length);
        console.log(keys, values);
        console.log("----------------------------------");
    }
    for (let i = 0; i < keys.length; i++) retval[keys[i]] = (values[i] === "NULL" ? null : values[i]);
    return retval;
}

// WORKING CODE
// get the input and output directories
const directories = { input: path.join(__dirname, "import"), output: path.join(__dirname, "export") };

// get the file names
const files = fs.readdirSync(directories.input).filter(file => file.endsWith(".csv"));

// read each file and transfor its data to json
files.map(file => {

    // get the file contents
    const filePath = path.join(directories.input, file)
    const [ keys, lines ] = reader(filePath);

    // read the lines of the current file
    const output = [];
    lines.map((line, index) => {
        if (line.length > 0) output.push(prepare(keys, line.split(";")));
        else console.log("zero line length found at:", index);
    })

    // create ouput folder
    if (!fs.existsSync(directories.output)) fs.mkdirSync(directories.output);

    // write the output to a json file
    fs.writeFileSync(path.join(directories.output, file.replace(".csv", ".json")), JSON.stringify(output))

})

// insert the data of those jsonfile into the database