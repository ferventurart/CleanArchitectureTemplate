var filterPaginate = (parametro, valor) => {
    let url = new URL(window.location.href);
    let params = new URLSearchParams(url.search);

    if (valor == undefined || valor == "") {
        params.delete(parametro);
        if (params.toString() == "") {
            location.href = url.origin + url.pathname + `?${params.toString()}`;
        } else {
            location.href = url.origin + url.pathname;
        }
    } else {
        url.searchParams.set(parametro, valor);
        location.href = url;
    }
}