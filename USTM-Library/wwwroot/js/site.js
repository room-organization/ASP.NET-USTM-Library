const btn_print = document.getElementById("btn_print")

btn_print.addEventListener("click", (evt) => {
    const content = document.getElementById('content').innerHTML

    let styl = "<style>"
    styl += ".container {width:500px;margin:50px auto;dont-family:Arial,sans-serif;}"
    styl += "h1 {font - size: 18px;  margin - bottom: 20px;"
    styl += ".label {  font - weight: bold;}"
    styl += "</style>"

    const wind = window.open('', '', 'height=700,wifht=700')

    wind.document.write('<html><head>')
    wind.document.write('<title>Comprovativo de Reserva</title>')
    wind.document.write(styl)
    wind.document.write('</head><body>')
    wind.document.write(content)
    wind.document.write('</body></html>')

    wind.print()
})