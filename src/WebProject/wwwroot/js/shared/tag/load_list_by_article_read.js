function tagListByArticleLoad() {

    fetch(`/Library/TagApi/GetTagsByArticleAndUserId?userId=${userId}&ArticleId=${articleId}`)
        .then(response => response.json())
        .then(tags => {

            tags.forEach(tag => {
                const div = document.querySelector("#tag-list");
                const context = `
                <span class="badge rounded-pill bg-warning">
                <a class="link-dark text-decoration-none"  href="#">#${tag.name}</a>
                </span>`;
                div.insertAdjacentHTML('beforeend', context);
            });

        });
}

tagListByArticleLoad();