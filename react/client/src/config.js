
// Function to store the token
export const setAPIToken = (token) =>{
    sessionStorage.setItem('authToken', token);
};

// Function to retrieve the token
export const getAPIToken = () =>{
    console.log("sessionStorage.getItem('authToken')::",sessionStorage.getItem('authToken'));
   return sessionStorage.getItem('authToken');
};
