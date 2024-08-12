export async  function createNewToken(credentials)
{
    try {
        console.log("inside createNewToken");
        const response = await fetch('https://localhost:7200/api/authentication/authenticate', {
          method: 'POST', 
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(credentials),
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // const data = await response.json();
        // console.log("data::", data);

        const text = await response.text();
        console.log("Raw response:", text);

        // Attempt to parse JSON if response is text
        // let data;
        // try {
        // data = JSON.parse(text);
        // } catch (e) {
        //     throw new Error('Response is not valid JSON');
        // }
    
        // // Check if the response contains a token
        // if (!data.token) {
        //   throw new Error('Token not found in response');
        // }
    
        return text;

    } catch (error) {
        console.error('Error fetching token:', error);
        throw error; // Re-throw the error to be handled by the caller
    }
}
