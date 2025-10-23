import http from 'k6/http';
import { check, sleep } from 'k6';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';



export const options = {
  stages: [
    { duration: '30s', target: 20 }, // Ramp up to 20 virtual users over 30 seconds
  //  { duration: '1m', target: 20 },  // Stay at 20 virtual users for 1 minute
   // { duration: '10s', target: 0 },  // Ramp down to 0 virtual users over 30 seconds
  ],
};

export default function () {

  const url = 'http://localhost:5041/Client'; // Replace with your actual API endpoint

  const randomUsername = randomString(10); // Generates a 10-character random string
  const randomDocto = randomString(14); // Generates a 10-character random string
  const randomAge = randomIntBetween(18, 99);
    

  const payload = JSON.stringify({
    name: randomUsername,
    docto: randomDocto,
    "age": randomAge
  });

  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };


  //let res = http.get('https://test.k6.io'); // Replace with your target URL
  let res = http.post(url, payload, params);
  check(res, {
    'status is 201': (r) => r.status === 201,
  });

    console.log('Full Response Body:', res.body);

  sleep(1); // Pause for 1 second between iterations
}