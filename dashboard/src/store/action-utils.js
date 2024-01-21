function parseList (response) {
    if (response.status !== 200) throw error (response.message);
    let list = response.data;
    if (typeof list !== 'object') {
        list = [];
    }
    return list;
}

export default parseList;