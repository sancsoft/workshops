await asyncTimeout(1000);

var five1 = asyncTimeout(5000);
var five2 = asyncTimeout(5000);

await five1;
await five2;

var ten1 = asyncTimeout(10000);
var ten2 = asyncTimeout(10000);
var ten3 = asyncTimeout(10000);

await Promise.all([ten1, ten2, ten3]);

var three = asyncTimeout(3000);
var six = asyncTimeout(6000);

await Promise.race([three, six])

console.log('Done');

async function asyncTimeout(timeout) {
    return new Promise((resolve, reject) => {
        console.debug(`Waiting ${timeout}ms`);
        setTimeout(() => {
            console.debug(`Waited ${timeout}ms`);
            resolve();
        }, timeout);
    });
}