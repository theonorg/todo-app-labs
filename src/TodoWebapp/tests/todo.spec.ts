import { test, expect, Page } from '@playwright/test';

test.beforeEach(async ({ page }) => {
  await page.goto('/');
});

const TODO_ITEMS = [
  'buy milk',
  'send email'
];


test('check page loaded', async ({ page }) => {

  await expect(page).toHaveTitle('List of To-Do Items - TodoWebapp');

  const locator = page.locator('#mainTitle');
  await expect(locator).toHaveText('To-Dos');
});

test.describe('create, get and edit todos', () => {
  test('should create two todos', async ({ page }) => {
    await page.getByRole('link', { name: 'Create New' }).click();
    await page.getByLabel('Title').click();
    await page.getByLabel('Title').fill(TODO_ITEMS[0]);
    await page.getByRole('button', { name: 'Create' }).click();

    await expect(page.getByRole('row').last()).toContainText(TODO_ITEMS[0]);

    await page.getByRole('link', { name: 'Create New' }).click();
    await page.getByLabel('Title').click();
    await page.getByLabel('Title').fill(TODO_ITEMS[1]);
    await page.getByRole('button', { name: 'Create' }).click();

    await expect(page.getByRole('row').last()).toContainText(TODO_ITEMS[1]);
  });

  test('should get details of last todo', async ({ page }) => {
    await createDefaultTodos(page);
    const row = await page.getByRole('row').last();

    await row.getByRole('link', { name: 'Details' }).click();

    const todo = await page.getByRole('definition').first();

    await expect(todo).toHaveText(TODO_ITEMS[TODO_ITEMS.length - 1]);
  });

  test('should edit details of last todo', async ({ page }) => {
    const newTitle = TODO_ITEMS[0] + " - Edited"
    await createDefaultTodos(page);
    const row = await page.getByRole('row').last();

    await row.getByRole('link', { name: 'Edit' }).click();

    await expect(page.getByLabel('Title')).toHaveValue(TODO_ITEMS[TODO_ITEMS.length - 1]);

    await page.getByLabel('IsComplete').check();
    await page.getByLabel('Title').click();
    await page.getByLabel('Title').fill(newTitle);

    const todo = await page.getByRole('definition').first();
    await page.getByRole('button', { name: 'Save' }).click();

    await expect(page.getByRole('row').last()).toContainText(newTitle);
  });

});

async function createDefaultTodos(page: Page) {
  for (const item of TODO_ITEMS) {
    await page.getByRole('link', { name: 'Create New' }).click();
    await page.getByLabel('Title').click();
    await page.getByLabel('Title').fill(item);
    await page.getByRole('button', { name: 'Create' }).click();
  }
}
