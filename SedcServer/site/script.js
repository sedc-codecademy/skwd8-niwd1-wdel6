document.write("Hi there");


document.getElementById("calculate").addEventListener("click", async () => {
    const first = document.getElementById("first").valueAsNumber;
    const second = document.getElementById("second").valueAsNumber;
    const operation = document.getElementById("operation").value;

    const url = `/calc-api/${operation}/${first}/${second}`;
    const response = await fetch(url);
    const result = await response.json();

    const resultDiv = document.getElementById("result");

    resultDiv.innerHTML = `The result of <strong>${result.Operation}</strong> between ${result.Arguments.join(" and ")} is <strong>${result.Result}</strong`
})
