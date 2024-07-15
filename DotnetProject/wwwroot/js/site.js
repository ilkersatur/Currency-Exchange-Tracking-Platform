async function getData() {
    d3.select("svg").remove();

    var choice = document.getElementById("selected").value;

    if (choice == "USD") { choice = 1 }
    else if (choice == "EUR") { choice = 2 }
    else { choice = 3 }

    var margin = { top: 0, right: 30, bottom: 90, left: 60 },
        width = 360 - margin.left - margin.right,
        height = 300;

    var svg = d3.select("body")
        .append("svg")
        .attr("width", width + margin.left + margin.right + 100)
        .attr("height", height + 50 + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

    let data = await fetch("http://localhost:5189/api/CachingCurrency").then(resp => resp.text());
    data = JSON.parse(data);

    //for (var i = 0; i < data.length; i++) {

    //    if (data[i].currencyid == 1) {

    //    }
    //}


    let counter = 0;

    var x = d3.scaleLinear()
        .domain(d3.extent(data, function (d) {
            if (d.currencyid == choice) {
                counter++;
                return counter;
            }
        }))
        .range([0, width]);
    svg.append("g")
        .attr("transform", "translate(0," + 300 + ")")
        .call(d3.axisBottom(x));




    var y = d3.scaleLinear()
        .domain([18.3, d3.max(data, function (d) {
            if (d.currencyid == choice) {
                return (d.forexselling / 10000);
            }
        })])
        .range([height, 0]);
    svg.append("g")
        .call(d3.axisLeft(y));
    counter = 0;

    //svg.append("path") // Add the valueline path.
    //    .attr("d", valueline(data)
    //    );


    svg.append("path")
        .datum(data)
        .attr("d", d3.line()
            .x(function (d) {
                if (d.currencyid == choice) {
                    counter++;
                }
                return x(counter)
            })
            .y(function (d) {
                if (d.currencyid == choice ) {

                    ay = d.forexselling
                }

                return y(ay / 10000)
            })
    )


    // X label
    svg.append('text')
        .attr('x', width / 2)
        .attr('y', height + 30)
        .attr('text-anchor', 'middle')
        .style('font-family', 'Helvetica')
        .style('font-size', 12)
        .text('Days');

    // Y label
    svg.append('text')
        .attr('text-anchor', 'middle')
        .attr('transform', 'translate(-40,' + 150 + ')rotate(-90)')
        .style('font-family', 'Helvetica')
        .style('font-size', 12)
        .text('Daily close (₺)');
}