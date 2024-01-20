const env = import.meta.env;
const API = env.VITE_REACT_APP_API || '/api';
export {API as default}