

function getAddress(cep){

    return new Promise((resolve, reject) => {
        if (cep.length === 8) {
            $.getJSON('https://viacep.com.br/ws/' + cep + '/json/', function (data) {
                if (!("erro" in data)) {
                    resolve(data);
                } else {
                    reject('CEP não encontrado.');
                }
            });
        } else {
            reject('CEP inválido.');
        }
    });

}